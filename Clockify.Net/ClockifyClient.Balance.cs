﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Clockify.Net.Models;
using Clockify.Net.Models.Balance;
using RestSharp;

namespace Clockify.Net;

public partial class ClockifyClient
{
	/// <summary>
	/// Get Balance by Policy Id.
	/// </summary>
	/// <returns></returns>
	public async Task<Response<BalanceDtoV1>> GetBalanceAsync(string workspaceId, string userId, BalanceRequest? balance = null)
	{
		var request = new RestRequest($"workspaces/{workspaceId}/balance/policy/{userId}");
		if (balance != null) request.AddJsonBody(balance);
		return Response<BalanceDtoV1>.FromRestResponse(await _ptoClient.ExecuteGetAsync<BalanceDtoV1>(request).ConfigureAwait(false));
	}
	
	/// <summary>
	/// Update Balance on workspace.
	/// </summary>
	public async Task<Response> UpdateBalanceAsync(string workspaceId, string policyId, UpdateBalanceRequest balance)
	{
		if (balance == null) throw new ArgumentNullException(nameof(balance));
		if (balance.UserIds == null) throw new ArgumentNullException(nameof(balance.UserIds));
		if (!balance.UserIds.Any()) throw new ArgumentNullException(nameof(balance.UserIds));
		if (balance.Value == null) throw new ArgumentNullException(nameof(balance.Value));

		var request = new RestRequest($"workspaces/{workspaceId}/balance/policy/{policyId}");
		request.AddJsonBody(balance);
		return Response.FromRestResponse(await _ptoClient.ExecuteAsync(request, Method.Patch).ConfigureAwait(false));
	}
	
	/// <summary>
	/// Get Balance by User Id.
	/// </summary>
	/// <returns></returns>
	public async Task<Response<BalancesDto>> GetBalanceByUserAsync(string workspaceId, string userId, BalanceRequest? balance = null)
	{
		var request = new RestRequest($"workspaces/{workspaceId}/balance/user/{userId}");
		if (balance != null) request.AddJsonBody(balance);
		return Response<BalancesDto>.FromRestResponse(await _ptoClient.ExecuteGetAsync<BalancesDto>(request).ConfigureAwait(false));
	}
}