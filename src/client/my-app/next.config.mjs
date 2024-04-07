/** @type {import('next').NextConfig} */
const nextConfig = {
  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: `http://0.0.0.0:${process.env.BACKEND_PORT ?? 8081}/:path*`
      }
    ]
  }
};

export default nextConfig;
