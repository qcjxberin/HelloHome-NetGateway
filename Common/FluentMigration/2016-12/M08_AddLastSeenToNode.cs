﻿using FluentMigrator;

namespace HelloHome.Common.FluentMigration
{
	[HhMigration(2016,12,12,21,52)]
	public class M08_AddLastSeenToNode : ForwardOnlyMigration
	{
		public override void Up ()
		{
			Create.Column ("lastSeen").OnTable ("Node").AsDateTime ().Nullable ();
		}
	}
}
