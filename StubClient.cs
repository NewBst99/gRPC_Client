using System;
using System.Threading.Tasks;

using Grpc.Core;
using myungbae;

public class StubClient
{
    private readonly Channel channel;
    private readonly GameService.GameServiceClient client;

    public StubClient(string host, int port)
    {
        channel = new Channel(host, port, ChannelCredentials.Insecure);
        client = new GameService.GameServiceClient(channel);
    }

    public async Task<UserInfoResponse> getUserInfo(int userid)
    {
        var request = new UserInfoRequest { Userid = userid };
        var response = await client.getUserInfoAsync(request);

        return response;
    }

    public async Task saveUserInfo(int userid, string nkname, float curexp, float maxexp, float userlevel,
                            float curhp, float maxhp, float curmp, float maxmp, float attpower, float statpoint, float skillpoint)
    {
        var request = new UserInfoRequest
        {
            Userid = userid,
            Nkname = nkname,
            Curexp = curexp,
            Maxexp = maxexp,
            Userlevel = userlevel,
            Curhp = curhp,
            Maxhp = maxhp,
            Curmp = curmp,
            Maxmp = maxmp,
            Attpower = attpower,
            Statpoint = statpoint,
            Skillpoint = skillpoint,
        };

        var response = await client.saveUserInfoAsync(request);
    }

    public async Task<UserLocationResponse> getUserLocation(int userid)
    {
        var request = new UserLocationRequest { Userid = userid };
        var response = await client.getUserLocationAsync(request);

        return response;
    }

    public async Task saveUserLocation(int userid, float xloc, float yloc, float zloc)
    {
        var request = new UserLocationRequest
        {
            Userid = userid,
            Xloc = xloc,
            Yloc = yloc,
            Zloc = zloc
        };

        var response = await client.saveUserLocationAsync(request);
    }

    public async Task<SkillRelationResponse> getSkillRelation(int userid)
    {
        var request = new SkillRelationRequest { Userid = userid };
        var response = await client.getSkillRelationAsync(request);

        return response;
    }

    public async Task saveSkillRelation(int userid, int skillid, int skilllevel)
    {
        var request = new SkillRelationRequest
        {
            Userid = userid,
            Skillid = skillid,
            Skilllevel = skilllevel
        };

        var response = await client.saveSkillRelationAsync(request);
    }

    public async Task<ItemRelationResponse> getItemRelation(int userid)
    {
        var request = new ItemRelationRequest { Userid = userid };
        var response = await client.getItemRelationAsync(request);

        return response;
    }

    public async Task saveItemRelation(int userid, string itemid, int quantity)
    {
        var request = new ItemRelationRequest
        {
            Userid = userid,
            Itemid = itemid,
            Quantity = quantity
        };

        var response = await client.saveItemRelationAsync(request);
    }

    public async Task ShutdownAsync()
    {
        await channel.ShutdownAsync();
    }
}
