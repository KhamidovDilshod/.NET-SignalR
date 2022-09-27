using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Realtime.API.Entities;

namespace Realtime.API.Extensions;

public class MongoExtension
{
    private readonly ILogger<MongoExtension> _logger;
    private readonly IMongoCollection<Message> _messageCollection;

    public MongoExtension(IOptions<MongoSettings> settings, ILogger<MongoExtension> logger)
    {
        _logger = logger;
        MongoClient client = new MongoClient(settings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(settings.Value.DatabaseName);
        _messageCollection = database.GetCollection<Message>(settings.Value.CollectionName);
    }

    public void MessageSeed()
    {
        if (Finder())
        {
            _logger.LogInformation("Migration started");
            _messageCollection.InsertMany(Seeder());
            _logger.LogInformation("Migration finished");
        }
    }

    private bool Finder()
    {
        return _messageCollection.Find(new BsonDocument()).ToList().Count == 0;
    }

    private List<Message> Seeder()
    {
        var list = new List<Message>
        {
            new Message
            {
                Id = "",
                Content = "",
                Sender = "",
                SenderTitle = "",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Type = "",
                ReadBy = "",
                SentAt = DateTime.Now,
                Chat = "",
            }
        };
        return list;
    }
}

public static class MongoRegistration
{
    public static IServiceCollection AddMongo(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<MongoSettings>(
            configuration.GetSection("MongoDB"));
        services.Configure<MongoSettings>(configuration.GetSection("mongodb://localhost:27017"));
        services.AddSingleton<MongoExtension>();
        return services;
    }
}