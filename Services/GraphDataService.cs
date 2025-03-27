using PapersApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PapersApi.Services;

public class GraphDataService
{
    private readonly IMongoCollection<GraphData> _graphDataCollection;

    public GraphDataService(
        IOptions<PapersDatabaseSettings> papersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            papersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            papersDatabaseSettings.Value.DatabaseName);

        _graphDataCollection = mongoDatabase.GetCollection<GraphData>(
            papersDatabaseSettings.Value.GraphDataCollectionName);
    }

    public async Task<List<GraphData>> GetAsync() =>
        await _graphDataCollection.Find(Builders<GraphData>.Filter.Empty).ToListAsync();

    public async Task<GraphData?> GetAsync(string id) =>
        await _graphDataCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<GraphData?> GetGraphDataByUser(string user) =>
        await _graphDataCollection.Find(x=> x.user == user).FirstOrDefaultAsync();


    public async Task CreateAsync(GraphData newGraphData) =>
        await _graphDataCollection.InsertOneAsync(newGraphData);

    public async Task UpdateAsync(string id, GraphData updatedGraphData) =>
        await _graphDataCollection.ReplaceOneAsync(x => x.Id == id, updatedGraphData);

    public async Task RemoveAsync(string id) =>
        await _graphDataCollection.DeleteOneAsync(x => x.Id == id);
}