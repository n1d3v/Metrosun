# Metrosun
> [!WARNING]
> This is still a work-in-progress, only the current page is implemented and a lot of functions are still missing, please report any bugs you find to us on the Issues tab above!

An open-source weather app for Windows Phone 8.1 that recreates the look of Bing Weather.<br>
This app uses the [OpenWeatherMap](https://openweathermap.org) API to fetch weather information, which means you need to provide your own API key.

<img width="360" height="640" alt="A screenshot of the current page in Metrosun." src="https://github.com/user-attachments/assets/dd373174-e3f3-4bae-ada0-4d3a70efa09a" />

# How to get an API key
This teaches you how to get an OpenWeatherMap API key, I'm using Firefox for this tutorial so your stuff may vary.
1. Visit the OpenWeatherMap website on your browser
2. Open DevTools (F12 on Firefox) and go to the 'Network' tab
3. Filter by 'XHR' requests and refresh the page
   - You should now see fewer requests
4. Copy the one that starts with `https://api.openweathermap.org/data/2.5/onecall`
   - You can copy it by right clicking on it, hovering over 'Copy Value' then 'Copy URL'
5. You should see a part like `&appid={AppID}` at the end of the URL, copy the {AppID} value
Congratulations! You now have an OpenWeatherMap API key.
# Credits
- Prognoza: The weather backgrounds used in Metrosun
- Kierownik223, timthewarriror_: Testing the application early
