divided project into 3 parts  - core - api - App

The project is structured into three main parts:



9-9-2026 Creating the infrastruture for AI

i have created 3 items



C O N T E X T  R E T R I E V A L

in the API i have the Contextrerieval service , which is responsible for handling requests and responses related to AI functionalities.

Purpose

Now we get to the actual machinery.

The interface said:

“You must be capable of retrieving context.”

The service says:

“Here's how I'm actually going to do it.”

Relationship:

IContextRetrievalService
↑
│ implements
│
ContextRetrievalService

Written in C# as:

public class ContextRetrievalService : IContextRetrievalService

That colon basically means:

ContextRetrievalService promises to fulfil the contract defined by IContextRetrievalService.



I C O N T E X T R E T R I E V A L

in the App we have the  Icontext retirevenal service - with the i standing for interface

Purpose

This says:

“Any context retrieval system used by SolOricle must provide this functionality.”

It doesn't care whether retrieval eventually uses:

keywords
TF-IDF
embeddings
cosine similarity
SQLite
Azure AI Search
Databricks
a vector database
something you invent yourself

It simply establishes a contract.

C O N T E X T  M A T C H

and finally in the core we have Context match nestled within the models class

Purpose

A ContextMatch represents one item that SolOricle has decided may be relevant to the user's current journal entry.

Imagine the user writes:

“I couldn't concentrate at work today and felt exhausted.”

SolOricle searches previous entries and finds:

“I barely slept last night and struggled to focus at work.”

We need an object capable of carrying that result around the application.

That's ContextMatch.


12 - 09- 26

Azure Database Creation and Connecting to API



I have now created a database to connect Soloricle to and Despite some issues finding a suitable server to host from it now seems created and functional. However in doing so its required me to install a new nuget package -  Microsoft.EntityFrameworkCore.SqlServer -- which is incompatible with the outdated  net8.0.

error fix - to fix this I've rolled back my Sql server install to .net.8.0.5 instead as it would be  a much larger job than time can allow to move everything else forward. 

 
W H E R E  I  L E F T  O F F 

	I am having some issues with azure - the website may possibly be down or it could be due to the fact I had to use the sweedish server but im stuck on  a loading screen which is stopping me from setting up the connection string 



11 - 11 - 26

Getting the Database Working (LocalDB) and Planning Better Emotion Detection



T O O L I N G

Started using Claude Code as a coding assistant. The Visual Studio extension for it failed to load
(it needs VS 17.14 and I'm on 17.11.5) - but the extension isn't needed, Claude Code runs from the terminal.
The 'claude' command wasn't recognised in VS's terminal only because VS was opened before it was installed - restarting VS fixes it.



W H Y  N O T  A Z U R E  ( F O R  N O W )

My Azure credits have run out. Instead I'm using SQL Server LocalDB - a free version of SQL Server
that came installed with Visual Studio and runs on my own PC.

Because Azure SQL and LocalDB are both SQL Server, the code is identical for both.
Moving back to Azure later only means changing the connection string - nothing else.



T H E  B U G  T H A T  W A S  S T O P P I N G  E V E R Y T H I N G

Program.cs asked for a connection string called "SolOricleDatabase" - but it didn't exist anywhere.
appsettings.json only had an old, unused SQLite one called "DefaultConnection".
So every attempt to save a journal entry would have crashed.



W H A T  I  C H A N G E D

1. Connection string -> stored in USER SECRETS, not appsettings.json

	dotnet user-secrets set "ConnectionStrings:SolOricleDatabase" "Server=(localdb)\MSSQLLocalDB;Database=SolOricle;Trusted_Connection=True;..."

	User secrets live in my Windows profile, outside the project folder, so they never get pushed to GitHub.
	This matters for Azure because that connection string contains a password.

2. Program.cs
	- Prints a clear WARNING at startup if the connection string is missing (instead of failing later with a confusing error)
	- Added EnableRetryOnFailure: if the database has a brief hiccup (Azure databases can "sleep"), EF Core waits and retries up to 5 times instead of crashing

3. appsettings.json - removed the unused SQLite connection string so it can't be confused with the real one

4. Migrations rebuilt for SQL Server
	A migration is a set of instructions EF Core uses to build the database tables.
	My old migration (JournalFix) was generated back when I used SQLite, so it used SQLite types
	(TEXT for IDs and dates, INTEGER) which SQL Server can't use properly - e.g. TEXT can't be a primary key,
	and the int IDs wouldn't auto-number.

	- Moved the old SQLite migration to _archive/SqliteMigrations (kept, not deleted)
	- Generated a new one:   dotnet ef migrations add InitialAzureSql
	- Applied it:            dotnet ef database update

	  Old (SQLite)            New (SQL Server)
	  Guid   -> TEXT          Guid   -> uniqueidentifier
	  DateTime -> TEXT        DateTime -> datetime2
	  string -> TEXT          string -> nvarchar(max)
	  int    -> INTEGER       int    -> int + Identity(1,1)  (auto-numbering)

	The new migration also picked up the UpdatedAt column that the old one was missing.



T E S T I N G

Ran the API, sent a test journal entry to POST /api/journal, then read it back with GET /api/journal/latest.
It saved and came back correctly - the full chain now works:

	App -> JournalController -> DbJournalService -> EF Core -> LocalDB



H O W  A N  E N T R Y  G E T S  S A V E D  ( I N  P L A I N  T E R M S )

	1. The app sends the title + text (CreateJournalEntryRequest - the "envelope")
	2. JournalController (the "front desk") checks the title and text aren't empty
	3. DbJournalService (the "back room") asks AnalysisService for the mood, then builds a proper JournalEntry
	   with an Id, timestamp and mood
	4. EF Core (the "translator") turns that C# object into SQL
	5. LocalDB (the "filing cabinet") stores it



W H A T  T H E  T E S T  R E V E A L E D

Test entry: "I felt anxious before my exam but happy afterwards."
Detected emotion: Positive  <- wrong

AnalysisService stops at the FIRST emotion word it finds ("happy" is checked first), so it ignores "anxious".
It also can't handle negation - "I'm not happy" would also come out Positive.



N E X T  -  P L A N N E D  F I X  ( N O T  I M P L E M E N T E D  Y E T )

Lexicon-based sentiment analysis with negation handling:
	- Each emotion has a word list (a "lexicon")
	- Read EVERY word and tally votes for each emotion instead of stopping at the first match
	- If the word before is a negator ("not", "never", "don't"), "not happy" votes Negative instead of Positive
	- Highest votes wins; a tie = "Mixed"; no votes = "Neutral"

	Text                                              Old        New
	"anxious before my exam but happy afterwards"     Positive   Mixed
	"I'm not happy today"                             Positive   Negative
	"worried and stressed, a bit happy"               Positive   Anxious
	"went to the shop"                                Neutral    Neutral

Known limitation to evaluate in the write-up: sarcasm ("oh great, another Monday") will still fool it.



F I X I N G  T H E  A P P  B U I L D

Error in VS: XLS0414 "The type 'System.Object' was not found" in ProcessingPage.xaml (IntelliSense only).
The XAML and code-behind matched fine (both SolOricle.App.views.ProcessingPage), so the page wasn't the problem.

A full Rebuild showed the real cause - the app project was trying to build 4 versions at once:
Android, iOS, MacCatalyst and Windows. iOS and Mac failed:
	- NETSDK1202: my PC builds with .NET 9, which no longer supports the net8 iOS/Mac targets
	- NETSDK1135: the iOS/Mac build tools aren't installed
And iOS/Mac apps can't be built on Windows anyway - Apple requires a Mac. They were just left over from the MAUI template.

Fix - SolOricle.App.csproj, only build what I actually use (Android + Windows):

	Before:  <TargetFrameworks>net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
	After:   <TargetFrameworks>net8.0-android</TargetFrameworks>
	         (the Windows line underneath is unchanged)

Result: Android and Windows both build with 0 errors.
The Platforms/iOS and Platforms/MacCatalyst folders are kept, just ignored, in case I ever get a Mac.



N O T E S  F O R  L A T E R

	- Most of the project isn't committed to git yet, and bin/obj build folders are being tracked - need a .gitignore and a commit
	- Program.cs and JournalController both define the journal endpoints (/journal and /api/journal) - should keep just one
	- The MAUI app's ReflectionPageViewModel makes its own HttpClient with a hard-coded URL instead of using JournalApiClient
	- Power BI can connect to the database at server (localdb)\MSSQLLocalDB, database SolOricle - useful for PL-300 practice



