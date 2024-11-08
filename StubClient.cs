using System;
using System.Threading.Tasks;

using Grpc.Core;
using myungbae;  // gRPC 생성된 파일들 포함

public class StubClient
{
    private readonly Channel channel;
    private readonly GameService.GameServiceClient client;

    public StubClient(string host, int port)
    {
        // Grpc.Core의 Channel을 사용하여 GrpcChannel을 대체
        channel = new Channel(host, port, ChannelCredentials.Insecure); // HTTP/2 보안을 비활성화 (개발 환경에서만 사용 권장)
        client = new GameService.GameServiceClient(channel);
    }

    public void getUserInfo(int userid)
    {
        var request = new UserInfoRequest { Userid = userid };
        var response = client.getUserInfo(request);

        Console.WriteLine(response.Checkmessage);
    }

    public void saveUserInfo(int userid, string nkname, int curexp, int maxexp, int userlevel, 
                            int curhp, int maxhp, int curmp, int maxmp, int attpower, int statpoint, int skillpoint, string savetime)
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
        
        var response = client.saveUserInfo(request);
        Console.WriteLine(response.Checkmessage);
    }

    public void getUserLocation(int userid)
    {
        var request = new UserLocationRequest { Userid = userid };
        var response = client.getUserLocation(request);

        Console.WriteLine(response.Checkmessage);
    }

    public void saveUserLocation(int userid, float xloc, float yloc, float zloc)
    {
        var request = new UserLocationRequest
        {
            Userid = userid,
            Xloc = xloc,
            Yloc = yloc,
            Zloc = zloc
        };

        var response = client.saveUserLocation(request);
    }

    public void getSkillRelation(int userid)
    {
        var request = new SkillRelationRequest { Userid = userid };
        var response = client.getSkillRelation(request);

        Console.WriteLine(response.Checkmessage);
    }

    public void saveSkillRelation(int userid, int skillid, int skilllevel)
    {
        var request = new SkillRelationRequest
        {
            Userid = userid,
            Skillid = skillid,
            Skilllevel = skilllevel
        };

        var response = client.saveSkillRelation(request);
    }

    public void getItemRelation(int userid)
    {
        var request = new ItemRelationRequest { Userid = userid };
        var response = client.getItemRelation(request);

        Console.WriteLine(response.Checkmessage);
    }

    public void saveItemRelation(int userid, string itemid, int quantity)
    {
        var request = new ItemRelationRequest
        {
            Userid = userid,
            Itemid = itemid,
            Quantity = quantity
        };

        var response = client.saveItemRelation(request);
    }

    public async Task ShutdownAsync()
    {
        await channel.ShutdownAsync();
    }
}
