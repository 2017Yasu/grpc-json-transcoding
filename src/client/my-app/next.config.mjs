/** @type {import('next').NextConfig} */
const nextConfig = {
  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: `http://0.0.0.0:${process.env.BACKEND_PORT ?? 8081}/api/:path*`
      }
    ]
  }
};

export default nextConfig;
