let ws = null;
const currentPrice = 1000;
let stepPrice = 50;
let quantity = 1;
const bidButton = document.getElementById('btn-bid');
const textWarningBid = document.getElementById('text-warning-bid');


// Tính tổng giá bid
function calculateTotal() {
    const totalPrice = currentPrice + stepPrice * quantity;
    document.getElementById('total-price').value = `$${totalPrice.toLocaleString('en-US')}`;
    document.getElementById('bid-total').innerText = `$${totalPrice.toLocaleString('en-US')}`;
}

// Tăng số lượng
function increaseQuantity() {
    const qtyInput = document.getElementById('quantity');
    qtyInput.value = parseInt(qtyInput.value) + 1;
    quantity = parseInt(qtyInput.value);
    calculateTotal();
}

// Giảm số lượng
function decreaseQuantity() {
    const qtyInput = document.getElementById('quantity');
    if (parseInt(qtyInput.value) > 1) {
        qtyInput.value = parseInt(qtyInput.value) - 1;
        quantity = parseInt(qtyInput.value);
        calculateTotal();
    }
}

// Hàm kiểm tra input là số
function validateNumberInput(e) {
    const inputElement = e.target;
    inputElement.value = inputElement.value.replace(/[^0-9]/g, ''); // Loại bỏ ký tự không phải số
    if (inputElement.value.startsWith('0')) {
        inputElement.value = inputElement.value.replace(/^0+/, '');
    }
}

// Hàm vô hiệu hóa nút Bid
function disableBidButton() {
    bidButton.disabled = true;
}

function enableBidButton() {
    bidButton.disabled = false;
}

// Xử lý thay đổi của step-price và quantity
document.getElementById('step-price').addEventListener('input', (e) => {
    validateNumberInput(e);
    stepPrice = parseInt(e.target.value) || 1;
    calculateTotal();
});

document.getElementById('quantity').addEventListener('input', (e) => {
    validateNumberInput(e);
    quantity = parseInt(e.target.value) || 1;
    calculateTotal();
});

// Đặt giá bid
function placeBid() {

    if (bidButton.disabled) {
        alert('You have already placed a bid. Please wait for your turn.');
        return;
    }
    const bidAmount = currentPrice + stepPrice * quantity;
    const confirmMessage = `Are you sure you want to place a bid of: $${bidAmount.toLocaleString('en-US')}?`;
    const isConfirmed = confirm(confirmMessage); // Hiển thị hộp thoại xác nhận

    if (isConfirmed) {
        const newBid = {
            userId: localStorage.getItem("userId"),
            auctionId: auctionId,
            fishId: fishId,
            price: bidAmount,
            isWinner: false
        };
        $.ajax({
            url: `${baseUrl}/userAuctions`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newBid),
            success: function (response) {
                if (response.status === 1) {
                    sendMessage();
                } else {
                    alert('Bid failed: ' + response.message);
                }
            },
            error: function () {
                alert('An error occurred while placing the bid.');
            }
        });
        alert(`You have placed a bid of: $${bidAmount.toLocaleString('en-US')}`);
    } else {
        alert('You have canceled the bid.');
    }
}

// Hàm tải danh sách detailPropsal từ server
function loadDetailProposal() {
    $.ajax({
        url: `${baseUrl}/detailProposal/${fishId}`,
        method: 'GET',
        contentType: 'application/json',
        success: function (response) {
            if (response.status === 1) {
                var detailProposal = response.data;
                console.log(detailProposal)

            }
            // else {
            //     $('#loadingMessage').html('<p style="color: red; font-weight: bold">' + response.message + '</p>');
            // }
        },
        error: function () {
            // $('#loadingMessage').html('<p style="color: red; font-weight: bold">An error occurred</p>');
        }
    });
}

// Hàm tải danh sách userAuctions từ server
function loadUserAuctions() {
    $('#loadingMessage').show();

    $.ajax({
        url: `${baseUrl}/userAuctions/${auctionId}/${fishId}`,
        method: 'GET',
        contentType: 'application/json',
        success: function (response) {
            $('#loadingMessage').hide();

            if (response.status === 1) {
                var userAuctions = response.data;

                // Nếu danh sách bid rỗng
                if (userAuctions.length === 0) {
                    var noBidsHtml = `
    <div class="my-2 card-past-bids-none">
        <div class="p-3 w-100">
            <dt class="fw-bold d-flex align-items-center justify-content-between">
                No Bids Yet
            </dt>
            <dd class="fw-semibold">Be the first to bid!</dd>
        </div>
    </div>`;
                    $('.card-past-bids-list').html(noBidsHtml);
                } else {
                    renderBids(userAuctions); // Render danh sách bid
                }
            } else {
                $('#loadingMessage').html('<p style="color: red; font-weight: bold">' + response.message + '</p>');
            }
        },
        error: function () {
            $('#loadingMessage').html('<p style="color: red; font-weight: bold">An error occurred</p>');
        }
    });
}

// Hàm render danh sách bids
function renderBids(bids) {
    $('.card-past-bids-list').empty();
    if (bids[0].userId == localStorage.getItem("userId")) {
        textWarningBid.style.display = "block"
        disableBidButton();
    } else {
        textWarningBid.style.display = "none"
        enableBidButton();
    }
    bids.forEach(function (auction, index) {
        // Kiểm tra nếu đây là phần tử đầu tiên (mới nhất)
        const itemClass = index === 0 ? 'card-past-bids-list-item' : 'card-past-bids-none';
        var auctionHtml = `
    <div class="my-2 ${itemClass}">
        <div class="px-3 py-2 w-100">
            <dt class="fs-3 fw-bold d-flex align-items-center justify-content-between">
                ${auction.mail} - $${auction.price.toLocaleString('en-US')}
            </dt>
            <dd class="fw-semibold">${new Date(auction.createDate).toLocaleString()}</dd>
        </div>
    </div>`;

        $('.card-past-bids-list').append(auctionHtml);
    });
}

function connectWebSocket() {
    ws = new WebSocket(`${wsUrl}/userAuctions/${auctionId}/${fishId}`)
    ws.onopen = () => {
        ws.send(JSON.stringify({ action: "join", auctionId: auctionId, fishId: fishId }));
    };

    ws.onmessage = (event) => {
        const message = JSON.parse(event.data);
        if (message.action === 'update') {
            disableBidButton();
            renderBids(message.data)
        }
    };
    ws.onclose = () => {
        console.log('WebSocket connection closed');
    };
    window.addEventListener('beforeunload', () => {
        if (ws && ws.readyState === WebSocket.OPEN) {
            ws.close();
        }
    });
}

function sendMessage() {
    if (ws && ws.readyState === WebSocket.OPEN) {
        ws.send(JSON.stringify({ action: "update user auctions", auctionId: auctionId, fishId: fishId }));
    }
}

$(document).ready(function () {

    disableBidButton();

    // Tải dữ liệu bid ngay khi trang load
    loadDetailProposal();
    loadUserAuctions();

    connectWebSocket();
});
