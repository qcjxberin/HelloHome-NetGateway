﻿using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class SubNodeConfiguration : EntityTypeConfiguration<NodePort>
	{
		public SubNodeConfiguration ()
		{
			ToTable ("SubNode");
			HasKey (_ => _.NodePortId);

			Property (_ => _.NodePortId).HasColumnName ("subNodeId").IsRequired ();
			Property (_ => _.NodeId).HasColumnName ("nodeId").IsRequired ();
			Property (_ => _.Number).HasColumnName ("number").IsRequired ();
			Property (_ => _.Name).HasColumnName ("name");
			Property (_ => _.PulseCount).HasColumnName ("pulseCount");

			HasMany(_ => _.PulseData).WithRequired().Map(_ => _.MapKey("subNodeId"));
		}
	}
}

