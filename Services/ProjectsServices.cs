using PapersApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace PapersApi.Services;

public class ProjectsService
{
    private readonly IMongoCollection<Project> _projectsCollection;

    public ProjectsService(
        IOptions<PapersDatabaseSettings> papersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            papersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            papersDatabaseSettings.Value.DatabaseName);

        _projectsCollection = mongoDatabase.GetCollection<Project>(
            papersDatabaseSettings.Value.ProjectsCollectionName);
    }

    public async Task<List<Project>> GetAsync() =>
        await _projectsCollection.Find(Builders<Project>.Filter.Empty).ToListAsync();

    public async Task<Project?> GetAsync(string id) =>
        await _projectsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Project>?> GetProjectByUser(string user) =>

    await _projectsCollection.AsQueryable().Where(x=> x.main_researcher == user).ToListAsync();


    public async Task CreateAsync(Project newProject) =>
        await _projectsCollection.InsertOneAsync(newProject);

    public async Task UpdateAsync(string id, Project updatedProject) =>
        await _projectsCollection.ReplaceOneAsync(x => x.Id == id, updatedProject);

    public async Task RemoveAsync(string id) =>
        await _projectsCollection.DeleteOneAsync(x => x.Id == id);
}