*** Settings ***
Resource  ../Resources/common_resource.robot
Resource  ../Resources/database_resource.robot
Library  JSONLibrary

*** Variables ***
${errorMessage}     Could not load the specified reports drilldown data
${badHttpCode}      400
${codev}    033
${BEARER_TOKEN}  eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6ImppYk5ia0ZTU2JteFBZck45Q0ZxUms0SzRndyJ9.eyJhdWQiOiIyMDhkMzQzMC0wM2E2LTQ0M2ItOGE5YS02Nzg5OTIxYTJkMTgiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMjcxYjVhYTgtMzQ0MC00ZjU1LWE1MDQtMmU5Zjg4MDVlOTY2L3YyLjAiLCJpYXQiOjE1OTg0MzkyNTQsIm5iZiI6MTU5ODQzOTI1NCwiZXhwIjoxNTk4NDQzMTU0LCJhaW8iOiJBVFFBeS84UUFBQUFoeTBBeXdpMFNQUzUyRUYzMW5vc2FweGlxSCtUTEN6MTNtQWMxTEdNblBUMkR4dmZOdDhNSXk3d1QzUzU3eWIwIiwiYXRfaGFzaCI6Im80RDFLeDFsY2M5MERrN1Y4YkM0MkEiLCJuYW1lIjoiQW5kcmllcyBLb29yemVuIiwibm9uY2UiOiI3ZjE0YTAxNDhlMTc0OTZmOTY4OGY4NWI3OWY3YjI1YiIsIm9pZCI6IjJkNGFmZjE0LTJiNmMtNDA4NS04NDg4LTE1NWM5NGY4OWJjMSIsInByZWZlcnJlZF91c2VybmFtZSI6ImFuZHJpZXNrQHJlZHBhbmRhc29mdHdhcmUuY28uemEiLCJyaCI6IjAuQVI4QXFGb2JKMEEwVlUtbEJDNmZpQVhwWmpBMGpTQ21BenRFaXBwbmlaSWFMUmdmQUZRLiIsInJvbGVzIjpbIlBvcnRhbC5IYXNBY2Nlc3MiXSwic3ViIjoieUxTZzhCbTd6WGJPZUhDNzFoNU9UaXVNaU94TWhMUDN5ZGcyN0R3UnVVSSIsInRpZCI6IjI3MWI1YWE4LTM0NDAtNGY1NS1hNTA0LTJlOWY4ODA1ZTk2NiIsInV0aSI6InJ0eEV3U0R4UDBPLThVY0tMcmtaQUEiLCJ2ZXIiOiIyLjAifQ.MO-1eGXi03B3WrC2p4MtKb2S6XkSc6Dv3aZNYjn2jGc9geRXfOsRPZrKux0dDY7DsxJUQM0UbaUV_LN9BM3H_V2KAlMjpSf-3uS0PVfFX6diy7fKyu1TzGvn_z6hGsNOPRxK_xOkJm0cS-3nPn3ZR_mfExpwhYTjsNW7qFEJaWL1UC5MGJoGUIm8pjZdhadX4S9ohgr_oJQOj-SLq1wwFgNvfrgJcE_PmVo7EnlIn_Y3OWL2NoMlYTPUzKrN1YOUTkouFX3-_49RhrNAxoNNcWE1dAb04FK15blUJh_lURlW0H_3unutQBUydL-QKPtw4GScm3RSh9Mf4Ygs8Pbamg

*** Test Cases ***
Create Database 
    [Tags]  createdb 
    [Documentation]  Create Database
    ${jsonResponse}=   database_resource.Create Database 
    ${length}=   Get Length   ${jsonResponse}
    ${code}=   Get Value From Json   ${jsonResponse}  $..code   
    Should Be Equal As Numbers  ${code[0]}  ${codev}
 