FROM node:16-alpine as build-stage
WORKDIR /app
COPY . ./
ENV NODE_OPTIONS=--max-old-space-size=16384

RUN npm install pnpm -g 
RUN pnpm i 
RUN npm run build


FROM nginx:1.17.3-alpine as production-stage
COPY --from=build-stage app/_nginx/nginx.conf /etc/nginx/nginx.conf
COPY --from=build-stage app/_nginx/env.js /etc/nginx/env.js
COPY --from=build-stage app/_nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage app/dist/ /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
