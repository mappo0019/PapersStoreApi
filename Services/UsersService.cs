using PapersApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace PapersApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(
        IOptions<PapersDatabaseSettings> papersDatabaseSettings)
    {
        MongoClient mongoClient = new MongoClient(
            papersDatabaseSettings.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            papersDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            papersDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetAsync() {

    

        return await _usersCollection.Find(Builders<User>.Filter.Empty).ToListAsync();
}



    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<User?> GetUserByName(string username) =>
        await _usersCollection.Find(x=> x.username == username).FirstOrDefaultAsync();

    public async Task<User?> GetUserByOpenAlexId(string id) =>
        await _usersCollection.Find(x=> x.openAlex_id == id).FirstOrDefaultAsync();

    public async Task<List<User>> GetUserByProject(string project)=>
         await _usersCollection.Find(Builders<User>.Filter.ElemMatch(z => z.project, a => a == project)).ToListAsync();

    public async Task CreateAsync(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
}