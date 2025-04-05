using PapersApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PapersApi.Services;

public class ProjectPapersService
{
    private readonly IMongoCollection<ProjectPapers> _projectPapersCollection;

    public ProjectPapersService(
        IOptions<PapersDatabaseSettings> papersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            papersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            papersDatabaseSettings.Value.DatabaseName);

        _projectPapersCollection = mongoDatabase.GetCollection<ProjectPapers>(
            papersDatabaseSettings.Value.ProjectPapersCollectionName);
    }

    public async Task<List<ProjectPapers>> GetAsync() =>
        await _projectPapersCollection.Find(Builders<ProjectPapers>.Filter.Empty).ToListAsync();

    public async Task<ProjectPapers?> GetAsync(string id) =>
        await _projectPapersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<ProjectPapers?> GetProjectPapersByProject(string project) =>
        await _projectPapersCollection.Find(x=> x.project == project).FirstOrDefaultAsync();


    public async Task CreateAsync(ProjectPapers newProjectPapers) =>
        await _projectPapersCollection.InsertOneAsync(newProjectPapers);

    public async Task UpdateAsync(string id, ProjectPapers updatedProjectPapers) =>
        await _projectPapersCollection.ReplaceOneAsync(x => x.Id == id, updatedProjectPapers);

    public async Task RemoveAsync(string id) =>
        await _projectPapersCollection.DeleteOneAsync(x => x.Id == id);
}