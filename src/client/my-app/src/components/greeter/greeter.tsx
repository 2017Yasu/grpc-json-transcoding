"use client";

import { useCallback, useEffect } from "react";
import { GreeterClient } from "../../../generated/api/GreetServiceClientPb";
import { HelloRequest } from "../../../generated/api/greet_pb";

export default function Greeter() {
  const greet = useCallback(async () => {
    const client = new GreeterClient(window.location.origin + "/api");
    const request = new HelloRequest();
    request.setName("Next.js");
    const result = await client.sayHello(request, null)
    alert(result.getMessage());
  }, []);

  useEffect(() => {
    console.log('connecting websocket...')
    const connection = new WebSocket(window.location.origin.replace("http", "ws") + "/ws");
    connection.onopen = () => {
      console.log('connection open!')
    }
    connection.onclose = () => {
      console.log('connection closed');
    }
    return () => {
      connection.close();
    }
  }, [])
  return <button onClick={greet}>Greet</button>;
}
