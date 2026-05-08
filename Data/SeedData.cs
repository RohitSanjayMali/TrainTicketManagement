using TrainTicketManagement.Models;

namespace TrainTicketManagement.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();

            // Only seed if no stations exist
            if (context.Stations.Any()) return;

            var stations = new List<Station>
            {
                new() { Name = "Mumbai",    Code = "CSTM" },
                new() { Name = "Delhi",     Code = "NDLS" },
                new() { Name = "Pune",      Code = "PUNE" },
                new() { Name = "Nashik",    Code = "NK"   },
                new() { Name = "Nagpur",    Code = "NGP"  },
                new() { Name = "Surat",     Code = "ST"   },
                new() { Name = "Ahmedabad", Code = "ADI"  },
                new() { Name = "Bangalore", Code = "SBC"  },
                new() { Name = "Chennai",   Code = "MAS"  },
                new() { Name = "Hyderabad", Code = "HYB"  },
                new() { Name = "Nandurbar", Code = "NDB"  },
            };

            context.Stations.AddRange(stations);
            await context.SaveChangesAsync();

            var trains = new List<Train>
            {
                new() { TrainName = "Deccan Express",    TrainNumber = "11007", TotalSeats = 200, PricePerSeat = 350,  FromStation = stations[0], ToStation = stations[2] },
                new() { TrainName = "Rajdhani Express",  TrainNumber = "12951", TotalSeats = 300, PricePerSeat = 1200, FromStation = stations[0], ToStation = stations[1] },
                new() { TrainName = "Shatabdi Express",  TrainNumber = "12001", TotalSeats = 250, PricePerSeat = 850,  FromStation = stations[1], ToStation = stations[4] },
                new() { TrainName = "Duronto Express",   TrainNumber = "12289", TotalSeats = 180, PricePerSeat = 950,  FromStation = stations[0], ToStation = stations[7] },
                new() { TrainName = "Garib Rath Express",TrainNumber = "12909", TotalSeats = 400, PricePerSeat = 450,  FromStation = stations[2], ToStation = stations[1] },
                new() { TrainName = "Surat Menu", TrainNumber = "12003", TotalSeats = 400, PricePerSeat = 35,  FromStation = stations[5], ToStation = stations[10] },
            };

            context.Trains.AddRange(trains);
            await context.SaveChangesAsync();
        }
    }
}
