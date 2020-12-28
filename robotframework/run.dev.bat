rem Insert your Jwt Token here

 
SET BEARER=SetBearerHere
rem if dev testing use your localhost e.g: http://localhost:5001/api
rem if qa testing use http://reddock03:8030

echo ON
SET APIURL=http://reddock03:8030
 
python -m robot --output results/report.xml -v BASE_URL:"%APIURL%" -v BEARER_TOKEN:"%BEARER%"  tests/url.robot

explorer.exe report.html
