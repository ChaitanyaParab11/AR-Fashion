#define BLYNK_PRINT Serial
#include <ESP8266WiFi.h>
#include <BlynkSimpleEsp8266.h>

#define BLYNK_AUTH_TOKEN "lBRjn4ulJTGOXkQ8GGUj4j_P9ZJ5XGJC" //Enter your blynk auth token

char auth[] = BLYNK_AUTH_TOKEN;
char ssid[] = "Redmi";//Enter your WIFI name
char pass[] = "12345678";//Enter your WIFI password


BLYNK_WRITE(V0) {
  digitalWrite(D0, param.asInt());
}

void setup() {
  pinMode(D0, OUTPUT);
  Blynk.begin(auth, ssid, pass, "blynk.cloud", 80);

  Serial.begin(115200);
  Blynk.begin(auth, ssid, pass);    
  //pinMode(relaypin,OUTPUT);
}
 
void loop() {
  Blynk.run();
}
