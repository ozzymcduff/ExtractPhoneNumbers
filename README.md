# ExtractPhoneNumbers

ExtractPhoneNumbers extracts phone numbers from text.

## Installation

Add the `ExtractPhoneNumbers` nuget package to your app. For example, using Package Manager Console:

        $ Install-Package ExtractPhoneNumbers

## Examples

Extract an international number from a text:

```
var tokens = MatchToken.Tokens("Hey my number is +1-312-555-1212 and my mom answers the phone").ToArray();
# => [#<ExtractPhoneNumbers::MatchToken Type=Text Value=Hey my number is >,
	  #<ExtractPhoneNumbers::MatchToken Type=Number Value=+1-312-555-1212 >,
	  #<ExtractPhoneNumbers::MatchToken Type=Text Value=and my mom answers the phone >]
```

If you only care about the phone numbers:

```
var tokens = PhoneNumbers.Extract("Hey my number is +1-312-555-1212 and my mom answers the phone").ToArray();
# => [+1-312-555-1212]
```

## Caveats

ExtractPhoneNumbers currently does not parse emergency numbers or SMS short code numbers.

## Development
The ExtractPhoneNumbers source code is [hosted on GitHub](https://github.com/wallymathieu/ExtractPhoneNumbers). You can check out a copy of the latest code using Git:

    $ git clone https://github.com/wallymathieu/ExtractPhoneNumbers.git

### License

Copyright &copy; 2013 Oskar Gewalli

Released under the MIT license. See [`LICENSE`](LICENSE) for details.
