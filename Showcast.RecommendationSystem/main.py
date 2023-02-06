from typing import Union

from fastapi import FastAPI
import uvicorn

from RecSys import RecSys

app = FastAPI()

# Setting up recommendation system.
rec_sys = RecSys.from_csv()
rec_sys.train()


@app.get("/relative/")
async def load_relative(n: Union[str, None] = None):
    return rec_sys.evaluate(n, recommendations=10)

if __name__ == "__main__":
    uvicorn.run(app, host="localhost", port=5002)
