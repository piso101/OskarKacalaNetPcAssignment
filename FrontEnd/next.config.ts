import { NextConfig } from "next";

const nextConfig: NextConfig = {
  webpack: (config, { isServer }) => {
    if (!isServer) {
      config.watchOptions = {
        poll: 100,
        aggregateTimeout: 300,
      };
    }
    return config;
  },
};

export default nextConfig;