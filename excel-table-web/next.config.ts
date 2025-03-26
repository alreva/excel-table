import type { NextConfig } from "next";
import {loadEnvConfig} from "@next/env";

loadEnvConfig(process.cwd());

const nextConfig: NextConfig = {
  // async rewrites() {
  //   return [
  //     {
  //       source: '/api/graphql',
  //       destination: `${process.env.BACKEND_API_URL}/graphql`,
  //     },
  //   ];
  // },
};

export default nextConfig;
