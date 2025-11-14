
const fetchData = (url, requestOptions) =>
{
    return fetch(url, requestOptions)
        .then((response) =>
        {
            if (!response.ok)
            {
                throw new Error(`Network response was not ok: ${response.status} ${response.statusText}`);
            }
            return response.json();
        })
        .catch((error) =>
        {
            throw error;
        });
};

export const apiGet = (url, params) =>
{
    const apiUrl = `${url}?${new URLSearchParams(params)}`;
    const requestOptions = {
        method: "GET",
    };

    return fetchData(apiUrl, requestOptions);
};

export const apiPost = (url, data) =>
{
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    };

    return fetchData(url, requestOptions);
};