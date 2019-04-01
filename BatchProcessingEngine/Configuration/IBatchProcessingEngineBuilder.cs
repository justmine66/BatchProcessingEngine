using Microsoft.Extensions.DependencyInjection;

namespace BatchProcessingEngine.Configuration
{
    /// <summary>
    /// An interface for configuring <see cref="BatchProcessingEngine"/> providers.
    /// </summary>
    public interface IBatchProcessingEngineBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where mda services are configured.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
