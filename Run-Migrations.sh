MigrationName="InitialCommit"

SourceProjectName="Showcast.Application"
TargetProjectName="Showcast.Infrastructure"

CommandArguments="-s ./$SourceProjectName/ --project ./$TargetProjectName/"

# Add migration.
dotnet ef migrations add $MigrationName $CommandArguments

# Updating migration.
dotnet ef database update $MigrationName $CommandArguments