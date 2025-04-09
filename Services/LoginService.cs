using PapersApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PapersApi.Services;

public class LoginService
{
    private readonly IMongoCollection<Login> _loginCollection;

    public LoginService(
        IOptions<PapersDatabaseSettings> papersDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            papersDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            papersDatabaseSettings.Value.DatabaseName);

        _loginCollection = mongoDatabase.GetCollection<Login>(
            papersDatabaseSettings.Value.LoginCollectionName);
    }

    public async Task<List<Login>> GetAsync() =>
        await _loginCollection.Find(Builders<Login>.Filter.Empty).ToListAsync();

    public async Task<Login?> GetAsync(string id) =>
        await _loginCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Login?> GetLoginByUser(string user) =>
        await _loginCollection.Find(x=> x.user == user).FirstOrDefaultAsync();


    public async Task CreateAsync(Login newLogin) =>
        await _loginCollection.InsertOneAsync(newLogin);

    public async Task UpdateAsync(string id, Login updatedLogin) =>
        await _loginCollection.ReplaceOneAsync(x => x.Id == id, updatedLogin);

    public async Task RemoveAsync(string id) =>
        await _loginCollection.DeleteOneAsync(x => x.Id == id);
}