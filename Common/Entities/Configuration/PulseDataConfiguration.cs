﻿using System;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloHome.Common.Entities.Configuration
{
	public class PulseDataConfiguration : EntityTypeConfiguration<PulseHistory>
	{
		public PulseDataConfiguration ()
		{
			ToTable ("PulseData");
			HasKey (_ => _.Id);
			Property (_ => _.Id).HasColumnName ("id").IsRequired ().HasDatabaseGeneratedOption (DatabaseGeneratedOption.Identity);	
			Property (_ => _.Timestamp).HasColumnName ("timestamp");
			Property (_ => _.NewPulses).HasColumnName ("newPulses");
			Property (_ => _.NewValue).HasColumnName ("newValue");
		}
	}
}

