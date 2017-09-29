# eNeg OfferApp

## Description

OfferApp supports the user during an negotiation with automatic suggestions for new offers or counteroffers to send to his counterpart. A basic offer and a target value based on the preferences of the user (can be suggested by [StrategyApp](https://github.com/ivconsult/eNeg-StrategyApp)) or entered by the user manually) is needed to setup the OfferApp. The basic offer can be the last offer received or sent or also a combination both. The OfferApp automatically suggest a new offer baed on the requirements to reach the target value of the user as well as to be as close as possible to the basic offer (last position of the counterpart). Especially in negotiations with many issues and 

factors this app can be very helpful.

OfferApp is a silverlight web based platform with desktop add-on extension, MVVM framework applied with n-tier layers.

* RIA Services
* Entity Framework
* Logging
* Excption Handling
* MEF

For more details please find the [Software Requirements Specification](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/SRS_eNeg_Negotiation_Framework.docx), the [Technical Design Specification](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/eNeg_TDS_KR.docx), the [Architecture](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/eNEG%20Infrastructure%20logical%20Architecture.docx) and some videos explaining the uses cases of eNeg in [eNeg Documentation](https://github.com/ivconsult/eNeg/tree/master/eNeg%20Documentation).

## Features

OfferApp provides the following features to [eNeg](https://github.com/ivconsult/eNeg) users:

* OfferApp generates offers that most probably lead to better negotiation results
* OfferApp advices the user which offers he should make. It suggests certain offers or counteroffer based on the targets of the user.
* Personal guide to automatic and best possible negotation result

## Setting Development Environment

* A .NET Integrated Development Environment (IDE) such as Visual Studio or the free Visual Web Developer Express
* Install Microsoft Silverlight runtime for [windows](https://go.microsoft.com/fwlink/?LinkId=229324). (This is the runtime that’s required for Silverlight applications).
* Install [Silverlight Toolkit](https://silverlight.codeplex.com/releases/view/78435)
* Install [Silverlight SDK](https://www.microsoft.com/en-us/download/details.aspx?id=28359)
* Install [Silverlight Tools for VS 2010](https://www.microsoft.com/en-us/download/details.aspx?id=28358) (Optional)
* Install [Expression Blend](https://www.microsoft.com/en-eg/download/details.aspx?id=3062). This is a design tool that allows users to interact with Silverlight.

## References
### Following open-source projects were used:
* [Clog Client Logging](http://clog.codeplex.com) under [LGPL](http://clog.codeplex.com/license)
* [MVVM Light Toolkit](http://www.mvvmlight.net) under [MIT License](http://mvvmlight.codeplex.com/license)
* [iTextSharp](https://github.com/itext/itextsharp) under [AGPL](https://github.com/itext/itextsharp/blob/develop/LICENSE.md)
* [Rhino Mocks](https://github.com/ayende/rhino-mocks) under [BSD 3-clause "New" or "Revised" License](https://github.com/ayende/rhino-mocks/blob/master/license.txt)
* [silverPDF](https://silverpdf.codeplex.com/) under [MIT License](https://silverpdf.codeplex.com/license)
