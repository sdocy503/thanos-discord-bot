using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using System.Threading;

namespace RiskBot.Modulues
{
    public class mainThanos : InteractiveBase
    {
        Random rnd = new Random();
        [Command("snap", RunMode = RunMode.Async)]
        public async Task snapTime()
        {
            var senderId = Context.User.Id;
            var sender = Context.Guild.GetUser(senderId);
            if(!sender.GuildPermissions.Administrator && !sender.GuildPermissions.BanMembers)
            {
                await ReplyAsync("You do not have permission to snap.");
            }
            else
            {
                List<ulong> allUsersId = new List<ulong>();
                List<string> allUsersName = new List<string>();

                //Gets all users
                var users = Context.Guild.Users;
                foreach (var user in users)
                {
                    allUsersId.Add(user.Id);
                    allUsersName.Add(user.Mention);
                    try
                    {
                        await Discord.UserExtensions.SendMessageAsync(user, $"Rejoice, for Thanos has arrived to balance the discord server: {Context.Guild.Name}.  If you are one of the many who were sacrificed know that your sacrifice will help maintain the stability of the server.  But there is a chance you can come back, if you do want to come back try contacting {Context.User.Username}.");
                    }
                    catch
                    {

                    }
                    //await ReplyAsync(user.Id.ToString());
                }

                //Randomizes users
                int n = allUsersId.Count;
                while (n > 1)
                {
                    n--;
                    int k = rnd.Next(n + 1);
                    ulong value = allUsersId[k];
                    string valueName = allUsersName[k];
                    allUsersId[k] = allUsersId[n];
                    allUsersName[k] = allUsersName[n];
                    allUsersId[n] = value;
                    allUsersName[n] = valueName;
                }

                //Print out the randomized list
                /*for (int i = 0; i < allUsers.Count; i++)
                {
                    await ReplyAsync(allUsers[i].ToString());
                }*/

                double userCount = allUsersId.Count;
                userCount = userCount / 2;

                int placeCounter = 0;
                int killCounter = 0;
                bool success = true;
                while (killCounter < userCount)
                {
                    if (placeCounter > allUsersId.Count)
                    {
                        await ReplyAsync("Thanos can't ban enough people to balance the server, he has tried his best though.");
                        success = false;
                        break;
                    }
                    try
                    {
                        await Context.Guild.AddBanAsync(allUsersId[placeCounter], 0);
                        await ReplyAsync($"{allUsersName[placeCounter]} was snapped by Thanos for the greater good of the server.");
                        killCounter++;
                        
                    }
                    catch(Exception e)
                    {
                        //Console.Write(e);
                    }
                    placeCounter++;
                }

                if (success)
                {
                    await ReplyAsync("Thanos has balanced your server.");
                }
                //await ReplyAsync();
            }
        }
    }

}
