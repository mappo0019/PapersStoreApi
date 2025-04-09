namespace PapersApi.Models;

public class PapersDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UsersCollectionName { get; set; } = null!;
    public string ProjectsCollectionName { get; set; } = null!;
    public string PapersCollectionName { get; set; } = null!;
    public string GraphDataCollectionName { get; set; } = null!;
    public string ProjectPapersCollectionName { get; set; } = null!;
    public string LoginCollectionName { get; set; } = null!;
}