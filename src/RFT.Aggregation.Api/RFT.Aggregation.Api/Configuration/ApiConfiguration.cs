namespace RFT.Aggregation.Api.Configuration
{
    public static class ApiConfiguration
    {
        /// <summary>
        ///   Configura elementos basicos da API
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiConfiguration(this IServiceCollection services)
        {            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        ///    Adiciona elementos da API para serem utilizados
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
