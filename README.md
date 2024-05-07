# KERI Auth Browser Extension
<span style="color:red">DRAFT</span>

The primary goal of this extension is to provide a highly secure way to "sign in" to certain websites. The browser extension under development will enable a user to interact with conforming websites, to authenticate with the user’s KERI identifier and to authorize with an ACDC credential they’ve been issued. 

It removes a class of problems website owners have from managing username-and-passwords or relying on federated identity providers that leak or correlate user usage patterns. 

Its passwordless design enables the user to create and manage their own stable identifiers (KERI AIDs),
signing keys, and credentials. It utilizes credentials issued by the website owners and/or other issuers they trust.

The browser extension, implemented for Chromium browsers, uses the [signify-ts](https://github.com/weboftrust/signify-ts) component to connect with an instance of [KERIA](https://github.com/weboftrust/keria), a KERI cloud agent, which safely manages the user’s AIDs, associated keys, and credentials. The user can choose a KERIA instance they host themselves or by a third party.

## Contents
- [Architecture](#architecture) 
  - [Service-worker](#service-worker)
  - [Browser Extension Action Icon/Button](#browser-extension-action-iconbutton)
  - [Content Script](#content-script)
  - [Integrated Web Page](#integrated-web-page)
  - [Blazor WASM Components](#blazor-wasm-components)
    - [Single Page App](#single-page-app)
    - [Views](#views)
    - [Services](#services)
- [Security considerations](#security-considerations)
- [Run for development](#run-for-development)
- [Roadmap](#roadmap)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Development Setup](#development-setup)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)
- [Contact Information](#contact-information)
- [Community](#community)

## Architecture
<!-- insert diagram -->
<!-- The browser extension is composed of the following components: -->

### Service-worker
* Runs background tasks.
* Sends and handles messages to/from the integrated webpage via injected content script.
* Persists configuration via chrome.storage.local.
* Communicates with KERIA.

### Browser Extension Action Icon/Button
* Button and context menu appears after install in the upper-right corner of the browser.
* Icon and/or its overlay text may change depending on state or awaiting notifications.
* Used to indicate the user’s intent and permission to interact with the current browser page.

### Content Script
* With the user’s permission, this script is injected into the active web page.
* Handles messages to/from the website via a JavaScript API that the web page also implements.
* Handles messages to/from the service-worker.

### Integrated Web Page
* Provided by the website owner, leveraging interfaces on the Content Script.

### Blazor WASM Components

#### Single Page App
* Program and page that runs when any non-trivial extension UI is visible.
* Includes services and views.

#### Views
* May appear as a popup (typically) or in a full tab

#### Services
* Interacts with service-worker (and indirectly the webpages).
* Communicates with KERIA agent.
* Persists configuration via chrome.storage.local.

## Security considerations
The following rules are enforced by design to ensure the security of the extension:
* The extension only sends signed headers to the website if the user has previously created a signing association with that website.
* The extension only sends signed headers to the website if the website is the active tab on the browser.
* The passcode is temporarily cached in the extension and is deleted after a few minutes.
* Messages from content script are allowed only if the content script belongs to the active tab.
* Direct messages from the website to the background script are only allowed for the active tab and if a signing association exists with the auto-authenticate flag enabled.
* Declare minimum required and optional permissions in the extension’s manifest.
* Never runs external or inline scripts (`eval()`).
* All sensitive data never reaches the content script or website.

## Run for development:
* TBD

# Roadmap
The goals for KERI Auth Brower Extension will evolve and will likely include interoperability with other KERI-related extensions and website JavaScript APIs.



<hr/>



## Installation
Step-by-step instructions for installing the extension:
- From a public repository (like Chrome Web Store or Firefox Add-ons).
- From the source code via `load unpacked` in developer mode.

## Usage
Detailed instructions on how to use the extension, including:
- Key features and how to access them.
- Screenshots or GIFs to illustrate functionality.
- Typical workflows and use cases.

## Configuration
Information on customizing the extension, if applicable:
- Configurable options.
- Instructions for changing settings.

## Development Setup
Instructions for setting up a development environment:
- Prerequisites (Node.js, npm, browser development tools, etc.).
- Cloning the repository and building the extension.
- Running tests, if applicable.

## Contributing
Guidelines for contributing to the project:
- How to submit issues and pull requests.
- Coding standards and best practices.
- Code of conduct and community guidelines.

## License
Details of the project's license (e.g., MIT, GPL):
- Include a short explanation of what it means.
- Link to the full license text.

## Acknowledgments
Mention of contributors, sponsors, or libraries used in the project.

## Contact Information
Contact information for the project maintainer(s):
- Email, GitHub profiles, or social media links.

## Community
Join the project's community on Discord:  
