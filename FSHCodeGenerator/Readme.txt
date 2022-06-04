FSH Source generator 
--------------------
This is based on FullStackHero   not suitable for other frameworks !
Run Migration if needed. 

IMPORTANT : Before we can start using this tool we need to scaffold the entities and context files.
			This is done by Powershell or in the package manager console.
			=>  command based on fullstackhero !!
			command =  Scaffold-DbContext "Data Source=(localdb)\mssqllocaldb;Initial Catalog=fullStackHeroDb;Integrated Security=True;MultipleActiveResultSets=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Context -Context SourceGenContext -ContextNamespace FSHCodeGenerator -Force
			!! there is a fake applicationdbcontext in the project this is needed because otherwise build fails.
			 the scaffold command has the -Force flag so the fake context is overwritten.	
			!! single backslash (double backslash = error)

Change appsettings with the appropriate settings
set the path to the dbcontext of your solution not the local fake one. 

The generated files are 'basic'
The Dto of the child entity takes only the first property of the parent entity 
The validator rules need to be adapted to your needs.
Some files need to be beautified  ( VS = Ctrl+k+d)
The get entity via dapper code for the 'Using mapster here throws a nullreference exception' need to be added .
In the controller code for export need to be added.

! ATTENTION
If you want to rerun this for a specific entity delete the folder of that entity in : FSH.WebApi.Application.Catalog
If you also want to rerun the permissions for this entity delete all references for this entity in permissions.cs

