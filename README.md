# Desolator

**Build Status Bagde**

A Asp.Net Core web application designed to serivce the needs of Ability Draft players.

**Project Status:**

- #1. AD Guide
- #2. Illusions Info
- #3. Heroes List and Details
- #4. Abilities List and Details
- #5. Match Analysis
- #6. Mock Drafter

## Getting Started

You will need to configure the Conection String
```
"ConnectionStrings": {
	"DefaultConnection": "Server=DESKTOP-563HJ3N\\SQLEXPRESS;Database=HGV.AD;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

The Follow application settings need to be set before the system will function
```
"Site": {
	"FounderSteamId": "",
	"DotaApiKey": ""
},
"ApplicationInsights": {
	"InstrumentationKey": ""
}
```

Both the Connection String and Settings can be found in the appsettings.json file but we recommand using [user secrets](https://docs.asp.net/en/latest/security/app-secrets.html) instead.

After which you will need to run the Seed methods in the Admin controller, then you can run the RegisterRecurringServices method to create the recurring jobs. (We use [hangfire](http://hangfire.io/) for our job system)

## License

See the LICENSE file.

## Help

If you have any questions, you can tweet us at [@DotaHGV](https://twitter.com/DotaHGV)

## Authors and Acknowledgements

Desolator is maintained and development by [HGV](http://www.highgroundvision.com), a leading Dota 2 data visualization and analysis web site. HGV's Team:

* [Jamie Webster](https://github.com/RGBKnights) 
* [Graham Clifford](https://github.com/gclifford)
