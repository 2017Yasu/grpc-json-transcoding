import { memo } from "react";
import dynamic from "next/dynamic";

const GreeterComponent = dynamic(() => import("./greeter"), { ssr: false });

const Greeter = memo(GreeterComponent);
export default Greeter;
