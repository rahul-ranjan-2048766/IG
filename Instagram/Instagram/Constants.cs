namespace Instagram
{
    public class Constants
    {
        public string USER_TOKEN { get; set; } = null!;
        public string ADMIN_TOKEN { get; set; } = null!;
        public string USER_NAME { get; set; } = null!;
        public string ADMIN_NAME { get; set; } = null!;
        public string MICROSERVICE_API { get; set; } = null!;
        public string AUTHENTICATE_CONTROLLER { get; set; } = null!;
        public string PROFILEPIC_CONTROLLER { get; set; } = null!;
        public string QUERY_CONTROLLER { get; set; } = null!;
        public string USER_CONTROLLER { get; set; } = null!;
        public string MESSAGE_CONTROLLER { get; set; } = null!;
        public string ADMIN_CONTROLLER { get; set; } = null!;
        public string POST_CONTROLLER { get; set; } = null!;
        public string COMMENT_CONTROLLER { get; set; } = null!;
        public string CHAT_CONTROLLER { get; set; } = null!;
        public string AUTHENTICATE_USER { get; set; } = null!;
        public string AUTHENTICATE_ADMIN { get; set; } = null!;

        public string AUTHENTICATION_SCHEME { get; set; } = null!;
        
        public string ADD_PROFILEPIC { get; set; } = null!;
        public string DELETE_PROFILEPIC { get; set; } = null!;
        public string DELETEALL_PROFILEPIC { get; set; } = null!;
        public string GET_PROFILEPIC { get; set; } = null!;
        public string GETALL_PROFILEPIC { get; set; } = null!;
        public string UPDATE_PROFILEPIC { get; set; } = null!;

        public string ADD_QUERY { get; set; } = null!;
        public string DELETE_QUERY { get; set; } = null!;
        public string DELETEALL_QUERY { get; set; } = null!;
        public string GET_QUERY { get; set; } = null!;
        public string GETALL_QUERY { get; set; } = null!;
        public string UPDATE_QUERY { get; set; } = null!;

        public string ADD_USER { get; set; } = null!;
        public string DELETE_USER { get; set; } = null!;
        public string DELETEALL_USER { get; set; } = null!;
        public string GET_USER { get; set; } = null!;
        public string GETALL_USER { get; set; } = null!;
        public string UPDATE_USER { get; set; } = null!;

        public string ADD_MESSAGE { get; set; } = null!;
        public string DELETE_MESSAGE { get; set; } = null!;
        public string DELETEALL_MESSAGE { get; set; } = null!;
        public string GET_MESSAGE { get; set; } = null!;
        public string GETALL_MESSAGE { get; set; } = null!;
        public string UPDATE_MESSAGE { get; set; } = null!;

        public string ADD_ADMIN { get; set; } = null!;
        public string DELETE_ADMIN { get; set; } = null!;
        public string DELETEALL_ADMIN { get; set; } = null!;
        public string GET_ADMIN { get; set; } = null!;
        public string GETALL_ADMIN { get; set; } = null!;
        public string UPDATE_ADMIN { get; set; } = null!;

        public string ADD_POST { get; set; } = null!;
        public string DELETE_POST { get; set; } = null!;
        public string DELETEALL_POST { get; set; } = null!;
        public string GET_POST { get; set; } = null!;
        public string GETALL_POST { get; set; } = null!;
        public string UPDATE_POST { get; set; } = null!;

        public string ADD_COMMENT { get; set; } = null!;
        public string DELETE_COMMENT { get; set; } = null!;
        public string DELETEALL_COMMENT { get; set; } = null!;
        public string GET_COMMENT { get; set; } = null!;
        public string GETALL_COMMENT { get; set; } = null!;
        public string UPDATE_COMMENT { get; set; } = null!;

        public string ADD_CHAT { get; set; } = null!;
        public string DELETE_CHAT { get; set; } = null!;
        public string DELETEALL_CHAT { get; set; } = null!;
        public string GET_CHAT { get; set; } = null!;
        public string GETALL_CHAT { get; set; } = null!;
        public string UPDATE_CHAT { get; set; } = null!;

        public Constants(IConfiguration config)
        {
            USER_TOKEN = config["User:Token"];
            USER_NAME = config["User:Name"];

            ADMIN_TOKEN = config["Admin:Token"];
            ADMIN_NAME = config["Admin:Name"];

            MICROSERVICE_API = config["APIs:Microservice:URL"];

            AUTHENTICATE_CONTROLLER = config["Controllers:Authenticate:Name"];
            PROFILEPIC_CONTROLLER = config["Controllers:ProfilePic:Name"];
            QUERY_CONTROLLER = config["Controllers:Query:Name"];
            USER_CONTROLLER = config["Controllers:User:Name"];

            MESSAGE_CONTROLLER = config["Controllers:Message:Name"];
            ADMIN_CONTROLLER = config["Controllers:Admin:Name"];

            POST_CONTROLLER = config["Controllers:Post:Name"];
            COMMENT_CONTROLLER = config["Controllers:Comment:Name"];

            CHAT_CONTROLLER = config["Controllers:Chat:Name"];


            AUTHENTICATION_SCHEME = config["Controllers:Authenticate:Scheme"];

            AUTHENTICATE_USER = config["Controllers:Authenticate:Endpoints:AuthenticateUser"];
            AUTHENTICATE_ADMIN = config["Controllers:Authenticate:Endpoints:AuthenticateAdmin"];
            
            ADD_PROFILEPIC = config["Controllers:ProfilePic:Endpoints:Add"];
            DELETE_PROFILEPIC = config["Controllers:ProfilePic:Endpoints:Delete"];
            DELETEALL_PROFILEPIC = config["Controllers:ProfilePic:Endpoints:DeleteAll"];
            GET_PROFILEPIC = config["Controllers:ProfilePic:Endpoints:Get"];
            GETALL_PROFILEPIC = config["Controllers:ProfilePic:Endpoints:GetAll"];
            UPDATE_PROFILEPIC = config["Controllers:ProfilePic:Endpoints:Update"];

            ADD_QUERY = config["Controllers:Query:Endpoints:Add"];
            DELETE_QUERY = config["Controllers:Query:Endpoints:Delete"];
            DELETEALL_QUERY = config["Controllers:Query:Endpoints:DeleteAll"];
            GET_QUERY = config["Controllers:Query:Endpoints:Get"];
            GETALL_QUERY = config["Controllers:Query:Endpoints:GetAll"];
            UPDATE_QUERY = config["Controllers:Query:Endpoints:Update"];

            ADD_USER = config["Controllers:User:Endpoints:Add"];
            DELETE_USER = config["Controllers:User:Endpoints:Delete"];
            DELETEALL_USER = config["Controllers:User:Endpoints:DeleteAll"];
            GET_USER = config["Controllers:User:Endpoints:Get"];
            GETALL_USER = config["Controllers:User:Endpoints:GetAll"];
            UPDATE_USER = config["Controllers:User:Endpoints:Update"];

            ADD_MESSAGE = config["Controllers:Message:Endpoints:Add"];
            DELETE_MESSAGE = config["Controllers:Message:Endpoints:Delete"];
            DELETEALL_MESSAGE = config["Controllers:Message:Endpoints:DeleteAll"];
            GET_MESSAGE = config["Controllers:Message:Endpoints:Get"];
            GETALL_MESSAGE = config["Controllers:Message:Endpoints:GetAll"];
            UPDATE_MESSAGE = config["Controllers:Message:Endpoints:Update"];

            ADD_ADMIN = config["Controllers:Admin:Endpoints:Add"];
            DELETE_ADMIN = config["Controllers:Admin:Endpoints:Delete"];
            DELETEALL_ADMIN = config["Controllers:Admin:Endpoints:DeleteAll"];
            GET_ADMIN = config["Controllers:Admin:Endpoints:Get"];
            GETALL_ADMIN = config["Controllers:Admin:Endpoints:GetAll"];
            UPDATE_ADMIN = config["Controllers:Admin:Endpoints:Update"];

            ADD_POST = config["Controllers:Post:Endpoints:Add"];
            DELETE_POST = config["Controllers:Post:Endpoints:Delete"];
            DELETEALL_POST = config["Controllers:Post:Endpoints:DeleteAll"];
            GET_POST = config["Controllers:Post:Endpoints:Get"];
            GETALL_POST = config["Controllers:Post:Endpoints:GetAll"];
            UPDATE_POST = config["Controllers:Post:Endpoints:Update"];

            ADD_COMMENT = config["Controllers:Comment:Endpoints:Add"];
            DELETE_COMMENT = config["Controllers:Comment:Endpoints:Delete"];
            DELETEALL_COMMENT = config["Controllers:Comment:Endpoints:DeleteAll"];
            GET_COMMENT = config["Controllers:Comment:Endpoints:Get"];
            GETALL_COMMENT = config["Controllers:Comment:Endpoints:GetAll"];
            UPDATE_COMMENT = config["Controllers:Comment:Endpoints:Update"];

            ADD_CHAT = config["Controllers:Chat:Endpoints:Add"];
            DELETE_CHAT = config["Controllers:Chat:Endpoints:Delete"];
            DELETEALL_CHAT = config["Controllers:Chat:Endpoints:DeleteAll"];
            GET_CHAT = config["Controllers:Chat:Endpoints:Get"];
            GETALL_CHAT = config["Controllers:Chat:Endpoints:GetAll"];
            UPDATE_CHAT = config["Controllers:Chat:Endpoints:Update"];
        }
        
    }
}


