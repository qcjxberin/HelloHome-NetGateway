﻿using System;
using Castle.MicroKernel.Registration;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Configuration.AppSettings;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using HelloHome.NetGateway.Processors;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.EmonCms;

namespace HelloHome.NetGateway.WindsorInstallers
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
				Component.For<IConfigurationProvider, ISerialConfigurationProvider, IEmonCmsConfiguration> ().ImplementedBy<AppSettingsBasedConfiguration> ()
			);

			//dbContext
			container.Register(Component.For<HelloHomeDbContext>().LifestyleSingleton());

			//Agents
			container.Register (Component.For<INodeGatewayAgent> ().ImplementedBy(_gatewayAgent));
			container.Register (Component.For<IEmonCmsAgent> ().ImplementedBy<EmonCmsAgent>());

			//Parsers & encoders
			container.Register(Classes.FromAssemblyContaining<IMessageParser>().BasedOn<IMessageParser>().WithServiceBase());
			container.Register(
				Classes.FromAssemblyContaining<IMessageEncoder>().BasedOn<IMessageEncoder>().WithServiceBase(),
				Component.For<PinConfigEncoder>()
			);

			//Processors
			container.Register(Classes.FromAssemblyContaining<IMessageProcessor>().BasedOn<IMessageProcessor>().WithServiceBase().Configure(_=>_.LifestylePerThread()));
			container.Register (Component.For<IRfNodeIdGenerationStrategy> ().ImplementedBy<FindHoleRfNodeIdGenerationStrategy> ());

			//EmonCmsUpdater
			container.Register(Component.For<EmonCmsUpdater>());
		}

		#endregion
	}
}

