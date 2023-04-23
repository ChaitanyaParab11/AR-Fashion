#define BLYNK_TEMPLATE_ID "TMPL3zsF49JoQ"
#define BLYNK_TEMPLATE_NAME "led v1"
#define BLYNK_AUTH_TOKEN "qQLoDi4YXyUyHrGVlnzk0iP8cs4qpxk4"

#define BLYNK_PRINT Serial
#include <ESP8266WiFi.h> 
 
#include <BlynkSimpleEsp8266.h>
 

char auth[] = BLYNK_AUTH_TOKEN;

char ssid[] = "Arif";  // type your wifi name
char pass[] = "Rizwana2130";  // type your wifi password

int relaypin = D4;
void setup()
{     
  Serial.begin(115200);
  Blynk.begin(auth, ssid, pass);    
  pinMode(relaypin,OUTPUT);
}

void loop()
{
  Blynk.run(); 
 }