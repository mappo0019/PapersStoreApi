using PapersApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PapersApi.Services;

public class PapersService
{
    private readonly IMongoCollection<Paper> _papersCollection;

    public PapersService(
        IOptions<PapersDatabaseSettings> papersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            papersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            papersDatabaseSettings.Value.DatabaseName);

        _papersCollection = mongoDatabase.GetCollection<Paper>(
            papersDatabaseSettings.Value.PapersCollectionName);
    }

    public async Task<List<Paper>> GetAsync() =>
        await _papersCollection.Find(Builders<Paper>.Filter.Empty).ToListAsync();

    public async Task<Paper?> GetAsync(string id) =>
        await _papersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Paper newPaper) =>
        await _papersCollection.InsertOneAsync(newPaper);

    public async Task UpdateAsync(string id, Paper updatedPaper) =>
        await _papersCollection.ReplaceOneAsync(x => x.Id == id, updatedPaper);

    public async Task RemoveAsync(string id) =>
        await _papersCollection.DeleteOneAsync(x => x.Id == id);
}