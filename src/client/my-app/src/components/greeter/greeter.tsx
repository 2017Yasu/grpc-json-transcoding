"use client";

import { useCallback } from "react";

export default function Greeter() {
  const greet = useCallback(async () => {
    const res = await fetch("/api/v1/greeter/next", { method: "GET" });
    const body = await res.json();
    alert(body.message);
  }, []);
  return <button onClick={greet}>Greet</button>;
}
