﻿using System;
using Castle.MicroKernel.Registration;
using NetHhGateway.Configuration;
using NetHhGateway.Configuration.AppSettings;
using NetHhGateway.Agents.NodeGateway.Parsers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using NetHhGateway.Processors;
using NetHhGateway.Entities;
using NetHhGateway.Agents.NodeGateway.Encoders;
using NetHhGateway.Logic.RfNodeIdGenerationStrategy;
using NetHhGateway.Agents.NodeGateway;

namespace NetHhGateway.WindsorInstallers
{
	public class DefaultInstaller : IWindsorInstaller
	{
		readonly Type _gatewayAgent;

		public DefaultInstaller (Type gatewayAgent)
		{
			this._gatewayAgent = gatewayAgent;
			
		}
		#region IWindsorInstaller implementation

		public void Install (Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
		{
			var kernel = container.Kernel;
			kernel.Resolver.AddSubResolver(new CollectionResolver(kernel));

			container.Register (Component.For<GatewayPipeline> ());

			//Configuration
			container.Register (
				Component.For<IConfigurationProvider> ().ImplementedBy<AppSettingsBasedConfiguration> (),
				Component.For<ISerialConfigurationProvider> ().ImplementedBy<AppSettingsBasedSerialConfiguration> ()
			);

			//dbContext
			container.Register(Component.For<HelloHomeDbContext>().LifestyleSingleton());

			//NodeGateway
			container.Register (Component.For<Agents.NodeGateway.INodeGatewayAgent> ().ImplementedBy(_gatewayAgent));

			//Parsers & encoders
			container.Register(Classes.FromAssemblyContaining<IMessageParser>().BasedOn<IMessageParser>().WithServiceBase());
			container.Register(
				Classes.FromAssemblyContaining<IMessageEncoder>().BasedOn<IMessageEncoder>().WithServiceBase(),
				Component.For<PinConfigEncoder>()
			);


			//Processors
			container.Register(Classes.FromAssemblyContaining<IMessageProcessor>().BasedOn<IMessageProcessor>().WithServiceBase().Configure(_=>_.LifestylePerThread()));
			container.Register (Component.For<IRfNodeIdGenerationStrategy> ().ImplementedBy<FindHoleRfNodeIdGenerationStrategy> ());

		}

		#endregion
	}
}
