# UrlShortener

## How to start?
- All you needs to do is simply run API project. To store application data there is used In Memory Database.

## Endpoints

- `POST /url-mappings` - create new url mapping
- `GET /url-mappings` - query all available url mappings
- `GET /r/{string}` - redirect to mapped url

## Key assumptions 
 - We are trying to protect redirect endpoint before guessing correct shortened url code
 - Solution is design to handle limited ammount of url mapping data.
Limitation is code used to create shortened urls.
Code is randomly build out of alphabet containing 62 characters `a-z A-Z 0-9` with length of 5.
Theoretically we could have up to `62^5 = 916 132 832` unique codes,
but because fact that we each time generate code randomly, at some point chance of that we will generate unique code will drastically drop.  
Some numbers, chance of duplicate code during generation: 
   - `50% after 35638`
   - `75% after 50400`
 - Overall concept of this design is to adjust length of code to fits needed capacity

## Future Ideas
- Threshold of difficulty for guess a valid redirect url  
- Extendable code length, depend on capacity / difficulty of generate new one / uniqueness threshold
- Use transaction to check and insert
- Create separate web service for handle only redirection requests ( we can then only handle url mapping codes without `/r/`)
- use SwaggerUI for better experience 
