
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class Node{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Ident {get; set;}
    public required string id {get; set;}
    public required int height {get; set;}
    public required int size {get; set;}
    public required string color {get; set;}
    //ERROR NO RECONOCE ID SIN IDENT
}