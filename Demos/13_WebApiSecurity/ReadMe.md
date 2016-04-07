# Web API Security Demo

## Using IdentityServer3 with ResourceOwner Flow

1. Configure Certificates
  - Open an admin command prompt and navigate to the tools folder
    + Located in the demo parent folder
  - Run: CreateDevRoot
  - Install DevRoot.pfx to Trust CA Root in local machine cert store
  - Run: CreateSslCert web.local
  - Install web.local.pfx to local machine cert store
    + Supply blank password when prompted
  - Open Notepad.exe as administrator
    + Open %windir%\System32\drivers\etc
    + Edit hosts file to add:
      127.0.0.1       web.local
  - Open HttpConfig.exe in tools folder
    + SSL: add web.local cert for port 4444
    + Permissions: add https://web.local:4444/core/ with your user account

2. Configure Oidc Server
  - Inspect config for clients, scopes, users
  - Start the server

3. Run the web service and invoke unsecure web api
  - Run the MvcWebApi project using IIS Express
  - Run the client to invoke the unsecure web api

4. Request an access token from the OIDC Server
  - Run the OidcServer project
  - Run the MvcWebApi project (for the unsecure service invocation)
  - Run the Client, enter username and password, request the token

5. Require authenticated users
  + Use a new auth policy builder to create a policy
    that requires users to be authenticated

    ```csharp
    // Require authenticated users
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    ```

  - Run the MvcWebApi project to use the unsecure api
    + You should now receive a 401 unauthorized response
    + Add an [AllowAnonymous] attribute to the UnsecureController
    + You should now get back the expected values
    + Accessing the secure api should still return a 401 response

6. Secure the web service using Jwt bearer tokens
  - Add the following dependency to project.json in the MvcWebApi project
    "Microsoft.AspNet.Authentication.JwtBearer": "1.0.0-rc1-final"
  - Add the following code to `Startup.Configure`
    + After UseIISPlatformHandler, before UseMvc

    ```csharp
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    app.UseJwtBearerAuthentication(options =>
    {
        options.Authority = "https://web.local:4444/core";
        options.RequireHttpsMetadata = false;

        options.Audience = "https://web.local:4444/core/resources";
        options.AutomaticAuthenticate = true;
    });
    ```

7. Run the MvcWebApi project, then start the NativeClient project
  - Use the client app to request an access token
  - Then use the access token to invoke the secure web api service
    + You should see the user's claims returned to the client

8. You can sniff the traffic from the native client to the secured web api service
   using Fidder
  - Append .fiddler to localhost in the program class of the native client
  - You can then paste the encoded token into the JWT debugger at http://jwt.io
    + To verify the token signature, select RS256 as the algorithm, then
      paste the base-64 encoded certificate public key from idsrv3test.cer

9. There are additional online samples you might want to check out:
  - AspNet 5 sample with MVC client and implicit flow:
    + https://github.com/IdentityServer/IdentityServer3.Samples/tree/master/source/AspNet5
  - IdentityServer3 samples based on OWIN
    + https://github.com/IdentityServer/IdentityServer3.Samples




