
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class Node{

    public required string id {get; set;}
    public required string name {get; set;}
    //ERROR NO RECONOCE ID
}