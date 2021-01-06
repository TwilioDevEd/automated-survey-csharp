<a href="https://www.twilio.com">
  <img src="https://static0.twilio.com/marketing/bundles/marketing/img/logos/wordmark-red.svg" alt="Twilio" width="250" />
</a>

# Automated Surveys using ASP.NET MVC

![](https://github.com/TwilioDevEd/automated-survey-csharp/workflows/NetFx/badge.svg)

This application demonstrates how to use Twilio and TwiML to perform automated phone surveys.

[Read the full tutorial here](https://www.twilio.com/docs/tutorials/walkthrough/automated-survey/csharp/mvc)!

## Running locally

This application requires [Visual Studio](https://www.visualstudio.com/) and SQLServer Express 2019 with [LocalDB enabled](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb).

1. Clone the repository and `cd` into it.

2. Open the solution file in Visual Studio.
![Open the solution file](https://raw.github.com/TwilioDevEd/automated-survey-csharp/main/solution-file.png)

3. Build the solution.
![Build the solution](https://raw.github.com/TwilioDevEd/automated-survey-csharp/main/build-solution.png)

4. Run `Update-Database` to execute the migrations.
*(Be sure to check that your database server name matches the one from the connection string on `Web.config`. For reference, default values where used upon SQLServer installation)*
![Run Update-Database](https://raw.github.com/TwilioDevEd/automated-survey-csharp/main/update-database.png)

Running the command `Update-Database` will run the migrations and run the `Seed` method, if you want to inspect this you can inspect SQL Server Object Explorer.

That's it!

## Configuring Twilio to call your application

### Exposing the application using ngrok

For this demo it's necessary that your local application instance is accessible from the Internet. The easiest way to accomplish this during development is using ngrok. The installer and the installation instructions are available [here](https://ngrok.com/).

Once you have the application running you can expose it to the wider internet by running the following command (port 1153 is the default port for this application).

```
ngrok http 1153 -host-header="localhost:1153"
```

### Configuring Twilio Webhooks

In order to receive incoming calls we need to first configure our [Twilio phone
number](https://www.twilio.com/user/account/phone-numbers/incoming).

![Configure Twilio Phone](https://raw.github.com/TwilioDevEd/automated-survey-csharp/main/configure-twilio-phone.png)

Click on the red number, and then set the Voice Request URL. Don’t forget to
save the changes.

![Set Voice Request URL](https://raw.github.com/TwilioDevEd/automated-survey-csharp/main/set-voice-request-url.png)

## Meta

* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](http://www.opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
