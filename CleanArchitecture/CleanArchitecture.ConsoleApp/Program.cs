

using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

StreamerDbContext dbContext = new();

await MultipleEntitiesQuery();
//await AddNewDirectorVideo();
//await AddNewActorVideo();
//await AddNewStreamerWithVideoId();
//await AddNewStreamerWithVideo();
//await TrackingAndNotTracking();
//await QueryLing();
//await QueryMethods();
//await QueryFilter();
//await AddNewRecords();
//QueryStreaming();

Console.WriteLine("Presione cualquier tecla para terminar mi perro");
Console.ReadKey();






async Task MultipleEntitiesQuery()
{
    //var videoWithActores = await dbContext!.Videos!.Include(q => q.Actores).FirstOrDefaultAsync(q => q.Id == 1);
    //var actor = await dbContext!.Actores!.Select(q => q.Nombre).ToListAsync();

    var videoWithDirector = await dbContext!.Videos!
        .Where(q => q.Director !=null)
        .Include(q => q.Director)
        .Select(q =>
               new
               {
                   Director_Nombre_Completo = $"{q.Director.Nombre} {q.Director.Apellido}",
                   Movie = q.Nombre
               }
                ).ToListAsync();

    foreach (var pelicula in videoWithDirector)
    {
        Console.WriteLine($"{pelicula.Movie}  - {pelicula.Director_Nombre_Completo}");
    }

}

async Task AddNewDirectorVideo()
{
    var director = new Director
    {
        Nombre = "Lorenzo",
        Apellido = "Basteri",
        VideoId = 3

    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync(); 
}


async Task AddNewActorVideo()
{
    var actor = new Actor
    {
        Nombre = "Bruce",
        Apellido = "Willis"

    };


    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var VideoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 2
    };

    await dbContext.AddAsync(VideoActor);
    await dbContext.SaveChangesAsync();  

}

async Task AddNewStreamerWithVideoId()
{
    

    var batmanForever = new Video
    {
        Nombre = "Batman Forever",
        StreamerId = 4
    };

    await dbContext.AddAsync(batmanForever);
    await dbContext.SaveChangesAsync();
}


async Task AddNewStreamerWithVideo()
{
    var hbo = new Streamer
    {
        Nombre = "HBO"
    };

    var hungerGames = new Video
    {
        Nombre = "hunger Games",
        Streamer = hbo
    };

    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync();
}


async Task TrackingAndNotTracking()
{
   var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
   var streamerWithNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking.Nombre = "Netflix";
    streamerWithNoTracking.Nombre = "Amazon Prime";

    await dbContext!.SaveChangesAsync();

}

async Task QueryLing()
{
    Console.WriteLine($"Ingrese el servicio de streaming ");
    var streamerNombre = Console.ReadLine();


    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach ( var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }


}

async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;

    var firstAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();

 
    var firstOrDefaultAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();

    var firstOrDefautl_v2 = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));

    var singleAsync = await streamer.Where(y => y.Id == 1).SingleAsync();
    var singleOrDefaultAsync = await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();


    var resultado = await streamer.FindAsync(1);
}

async Task QueryFilter()
{
    Console.WriteLine("Ingrese una compañia de streaming: ");
        var streamingNombre = Console.ReadLine();
    var streamers = await dbContext!.Streamers!.Where(x => x.Nombre.Equals(streamingNombre) ).ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

    //var streamerPartialResult = await dbContext!.Streamers!.Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync();
    var streamerPartialResult = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();

    foreach (var streamer in streamerPartialResult)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

}

async Task AddNewRecords()
{

    Streamer streamer = new()
    {
        Nombre = "Disney +",
        Url = "https://www.Disneyplus.com"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();


    var movies = new List<Video>
{
    new Video
    {
        Nombre = "Hercules",
        StreamerId = streamer.Id
    },

    new Video
    {
        Nombre = "Aladin",
        StreamerId = streamer.Id
    },
     new Video
    {
        Nombre = "Cars",
        StreamerId = streamer.Id
    },
      new Video
    {
        Nombre = "101 dalmatas",
        StreamerId = streamer.Id
    },
};

    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}