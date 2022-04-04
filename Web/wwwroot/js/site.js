

Array.from(document.querySelectorAll(".fa-thumbs-up, .fa-thumbs-down"))
    .forEach(x => x
        .addEventListener("click", Rate));


async function Rate(e) {
    const target = e.currentTarget;
    const set = target.dataset;
    const rate = parseInt(set.rate);
    const data = { "UserId": set.userId, "MessageId": set.msgId, "Rate": rate };
    const response = await fetch(
        'http://localhost:5105/Rate',
        {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });
    const rateSumContainer = rate > 0 ? target.nextElementSibling : target.previousElementSibling;
    const targetInverse = rate > 0 ? rateSumContainer.nextElementSibling : rateSumContainer.previousElementSibling;
    if (response.ok) {
        let rateSum = parseInt(rateSumContainer.textContent);
        rateSumContainer.textContent = target.classList.contains("rated")
                ? rateSum - rate
                : targetInverse.classList.contains("rated")
                    ? rateSum + 2 * rate
                    : rateSum + rate;
        target.classList.toggle("rated");
        if (targetInverse.classList.contains("rated"))
            targetInverse.classList.remove("rated");
    }
}