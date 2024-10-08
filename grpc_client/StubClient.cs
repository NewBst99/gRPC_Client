using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Grpc.Net.Client;
using myungbae;  // gRPC 생성된 파일들 포함

namespace grpc_client
{
    public class StubClient
    {
        private readonly GrpcChannel channel;
        private readonly GameService.GameServiceClient client;

        public StubClient(string host, int port)
        {
            channel = GrpcChannel.ForAddress("http://localhost:9090");
            client = new GameService.GameServiceClient(channel);
        }

        public void getUserInfo(int userid)
        {
            var request = new UserInfoRequest { Userid = userid };
            var response = client.getUserInfo(request);

            Console.WriteLine(response.Checkmessage);
            Console.WriteLine("User ID : " + response.Userid);
            Console.WriteLine("Nickname : " + response.Nkname);
            Console.WriteLine("Experience : " + response.Exp);
            Console.WriteLine("Save Time : " + response.Savetime);
        }

        public void saveUserInfo(int userid, string nkname, int exp, string savetime)
        {
            var request = new UserInfoRequest
            {
                Userid = userid,
                Nkname = nkname,
                Exp = exp,
                Savetime = savetime
            };

            var response = client.saveUserInfo(request);
            Console.WriteLine(response.Checkmessage);
        }

        public void getMapProgress(int userid)
        {
            var request = new MapProgressRequest { Userid = userid };
            var response = client.getMapProgress(request);

            Console.WriteLine(response.Checkmessage);
            Console.WriteLine("User ID : " + response.Userid);
            Console.WriteLine("User Location : " + response.Xloc + ", " + response.Yloc + ", " + response.Zloc);
            Console.WriteLine("Map Progress : " + response.Mapprogress);
        }

        public void saveMapProgress(int userid, int mapprogress, double xloc, double yloc, double zloc)
        {
            var request = new MapProgressRequest
            {
                Userid = userid,
                Mapprogress = mapprogress,
                Xloc = xloc,
                Yloc = yloc,
                Zloc = zloc
            };

            var response = client.saveMapProgress(request);
        }

        public void getItemRelation(int userid)
        {
            var request = new ItemRelationRequest { Userid = userid };
            var response = client.getItemRelation(request);

            Console.WriteLine(response.Checkmessage);
            Console.WriteLine("User ID : " + response.Userid);
            Console.WriteLine("Item ID : " + response.Itemid);
            Console.WriteLine("Item Name : " + response.Itemname);
            Console.WriteLine("Quantity : " + response.Quantity);
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

        public static void Main(string[] args)
        {
            var client = new StubClient("localhost", 9090);

            // Save User Info
            client.saveUserInfo(1, "user1", 80, "2024-10-08 21:57:15");
            client.saveUserInfo(2, "user2", 40, "2024-10-03 19:45:35");
            client.saveUserInfo(3, "user3", 58, "2024-06-07 15:19:41");
            client.saveUserInfo(4, "user4", 21, "2024-07-10 11:23:14");
            client.saveUserInfo(5, "user5", 52, "2024-03-07 15:35:24");

            // Save Map Progress
            client.saveMapProgress(1, 0, -40, -8, 0);
            client.saveMapProgress(2, 1, -160, -52, 9);

            // Save Item Relations
            client.saveItemRelation(1, "c100", 1);
            client.saveItemRelation(1, "c101", 2);
            client.saveItemRelation(2, "c100", 1);
            client.saveItemRelation(2, "c101", 1);

            client.getUserInfo(1);
            client.getMapProgress(1);
            client.getItemRelation(1);

            client.getUserInfo(2);
            client.getMapProgress(2);
            client.getItemRelation(2);

            client.ShutdownAsync().Wait();
        }
    }
}