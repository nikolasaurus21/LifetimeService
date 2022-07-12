using Microsoft.Extensions.DependencyInjection;

namespace LifetimeService {
    public interface IService
    {
        void Info();
    }

    public interface ISingleton : IService { }
    public interface IScoped : IService { }
    public interface ITransient : IService { }

    public class Operation : ISingleton, IScoped, ITransient
    {
        private Guid _operationId;
        private string _lifeTime;

        public Operation(string lifeTime)
        {
            _operationId = Guid.NewGuid();
            _lifeTime = lifeTime;

           
        }

        public void Info()
        {
            Console.WriteLine($"{_lifeTime}: {_operationId}");
           
        }
    }

    public class Singleton : Operation
    {
        public Singleton() : base("Singleton") 
        {

        }
    }
    public class Scoped : Operation
    {
        public Scoped() : base("Scoped")
        {

        }
    }
    public class Transient : Operation
    {
        public Transient() : base("Transient")
        {

        }
    
        static void Main(string[] args)
        {

            var service = new ServiceCollection();
            service.AddTransient<ITransient, Transient>();
            service.AddScoped<IScoped, Scoped>();
            service.AddSingleton<ISingleton, Singleton>();
            var serviceProvider = service.BuildServiceProvider();


            Console.WriteLine("Prvi:");

            serviceProvider.GetRequiredService<ITransient>().Info();
            serviceProvider.GetRequiredService<IScoped>().Info();
            serviceProvider.GetRequiredService<ISingleton>().Info();
            
            Console.WriteLine("Drugi:");

            serviceProvider.GetRequiredService<ITransient>().Info();
            serviceProvider.GetRequiredService<IScoped>().Info();
            serviceProvider.GetRequiredService<ISingleton>().Info();
            


           
        }


    }

}