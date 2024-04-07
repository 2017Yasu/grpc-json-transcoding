"use client";

import { useCallback } from "react";
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
  return <button onClick={greet}>Greet</button>;
}
