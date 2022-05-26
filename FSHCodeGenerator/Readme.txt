FSH Source generator 
--------------------
This is based on FullStackHero   not suitable for other frameworks !
Run Migration if needed. 

IMPORTANT : Before we can start using this tool we need to scaffold the entities and context files.
			This is done by Powershell or in the package manager console.
			command =  Scaffold-DbContext "Data Source=(localdb)\mssqllocaldb;Initial Catalog=fullStackHeroDb;Integrated Security=True;MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context SourceGenContext -ContextNamespace FSHCodeGenerator -Force
			!! there is a fake applicationdbcontext in the project this is needed because otherwise build fails.
			 the scaffold command has the -Force flag so the fake context is overwritten.	

			!! single backslash (double backslash = error)
Change appsettings with the appropriate settings

