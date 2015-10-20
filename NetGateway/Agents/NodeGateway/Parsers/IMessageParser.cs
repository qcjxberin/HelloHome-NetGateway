﻿
namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public interface IMessageParser
	{
		bool CanParse (byte[] record);
		Domain.Report Parse(byte[] record);
	}
}

