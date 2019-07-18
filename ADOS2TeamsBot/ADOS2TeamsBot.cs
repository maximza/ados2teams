using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema.Teams;
using System;
using ADOS2Teams.ADOSSearch;

namespace ADOS2Teams
{
    public class ADOS2TeamsBot : IBot
    {
        private readonly ADOSSearchHandler searchHandler;

        public ADOS2TeamsBot(ADOSSearchHandler sh)
        {
            this.searchHandler = sh;
        }

        public async Task OnTurnAsync(ITurnContext turnCtx, CancellationToken cancellationToken)
        {
            try
            {
                if (turnCtx.Activity != null && turnCtx.Activity.Type != null && turnCtx.Activity.Type == ActivityTypes.Invoke)
                {
                    
                    ITeamsContext teamsContext = turnCtx.TurnState.Get<ITeamsContext>();

                    MessagingExtensionQuery meQuery = null;
                    if (teamsContext.IsRequestMessagingExtensionQuery())
                    {
                        meQuery = teamsContext.GetMessagingExtensionQueryData();
                    }

                    InvokeResponse ir = new InvokeResponse
                    {
                        Body = new MessagingExtensionResponse
                        {
                            ComposeExtension = await searchHandler.GetSearchResultAsync(meQuery.Parameters[0].Value.ToString())
                        },
                        Status = 200,
                    };

                    await turnCtx.SendActivityAsync(
                        new Activity
                        {
                            Value = ir,
                            Type = ActivityTypesEx.InvokeResponse,
                        }).ConfigureAwait(false);
                }
                else
                    await turnCtx.SendActivityAsync(string.Format("Sorry! As of now I'm only support an invoke command, but you're trying to execute {0}", (turnCtx.Activity != null && turnCtx.Activity.Type != null) ? turnCtx.Activity.Type.ToString() : "empty activity"));
            }
            catch (Exception ex)
            {
                await turnCtx.SendActivityAsync($"Oppps, seems like an error: {ex.InnerException}. Try again or contact your IT admin");
            }
        }
    }
}